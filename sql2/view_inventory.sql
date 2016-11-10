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
	  ,COALESCE(gr.quantity,0) AS 'issued'
	  ,COALESCE(pol.quantity,0) AS 'purchased'
	  ,(COALESCE(gr.quantity,0) - COALESCE(pol.quantity,0)) AS 'quantity'
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
