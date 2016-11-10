-- inventory = issued goods - purchased

-- make sure you're using your own database
-- USE ISYS42833xx;
-- GO

IF OBJECT_ID('view_inventory', 'V') IS NOT NULL
    DROP VIEW view_inventory;
GO

CREATE VIEW view_inventory AS
SELECT [id]
      ,[name]
	  -- format comma separated integers
	  -- http://stackoverflow.com/a/27638248/4233593
	  -- NULL coalescer returns first non-null value
	  -- http://stackoverflow.com/a/13366500/4233593
	  ,FORMAT(COALESCE(gr.quantity,0), '#,0') AS 'issued'
	  ,FORMAT(COALESCE(pol.quantity,0), '#,0') AS 'purchased'
	  ,FORMAT(COALESCE(gr.quantity,0) - COALESCE(pol.quantity,0), '#,0') AS 'quantity'
      ,[description]
      ,[image]
      ,FORMAT(p.[price], 'C', 'en-us') AS 'price'
      ,[vendor]
FROM [ISYS4283].[dbo].[products] p

LEFT JOIN (
	SELECT [product] ,SUM([quantity]) AS 'quantity'
	FROM [ISYS4283].[dbo].[goods_receipts]
	GROUP BY [product] 
) gr ON p.id = gr.product

LEFT JOIN (
	SELECT [product] ,SUM([quantity]) AS 'quantity'
	FROM [ISYS4283].[dbo].[po_lines]
	GROUP BY [product]
) pol ON p.id = pol.product

WHERE [vendor] = SYSTEM_USER
