/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [id]
      ,[name]
      ,[description]
      ,[image]
      ,[price]
      ,[vendor]
	  ,COALESCE(gr.quantity,0) AS 'issued'
	  ,COALESCE(pol.quantity,0) AS 'purchased'
	  ,(COALESCE(gr.quantity,0) - COALESCE(pol.quantity,0)) AS 'quantity'
  FROM [ISYS4283].[dbo].[products] p
  LEFT JOIN (
	  SELECT
		  [product]
		  ,SUM([quantity]) AS 'quantity'
	  FROM [ISYS4283].[dbo].[goods_receipts]
	  GROUP BY [product] 
  ) gr
    ON p.id = gr.product
  LEFT JOIN (
	SELECT [product]
		  ,SUM([quantity]) AS 'quantity'
	  FROM [ISYS4283].[dbo].[po_lines]
	  GROUP BY [product]
  ) pol
    ON p.id = pol.product
  WHERE [vendor] = 'ISYS4283309'
  
  -- inventory = issued goods - purchased