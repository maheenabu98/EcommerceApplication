CREATE PROCEDURE GetCustomerOrderDetails 
    @CustomerId  NVARCHAR(50)
AS
  BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
      --gets the latest orderdate
      WITH LatestOrders
           AS (SELECT 
                   o.CUSTOMERID, 
                   MAX(o.ORDERDATE) AS LatestOrderDate
                 FROM ORDERS AS o
                 WHERE o.CUSTOMERID = @CustomerId
                 GROUP BY 
                   o.CUSTOMERID)
           SELECT 
               c.CUSTOMERID AS                                                      CustomerId, 
               c.FIRSTNAME AS                                                       FirstName, 
               c.LASTNAME AS                                                        LastName, 
               o.ORDERID AS                                                         OrderId, 
               o.ORDERDATE AS                                                       OrderDate, 
               CONCAT(c.HOUSENO, ', ', c.STREET, ', ', c.TOWN, ', ', c.POSTCODE) AS DeliveryAddress, 
               oi.ORDERITEMID AS                                                    OrderItemId, 
               oi.PRODUCTID AS                                                      ProductId, 
               p.PRODUCTNAME AS                                                     ProductName, 
               oi.PRICE AS                                                          Price, 
               oi.QUANTITY AS                                                       Quantity, 
               o.DELIVERYEXPECTED AS                                                DeliveryExpectedDate, 
               o.CONTAINSGIFT AS                                                    IsGift
             FROM CUSTOMERS AS c
             LEFT JOIN LatestOrders AS lo
               ON c.CUSTOMERID = lo.CUSTOMERID
             LEFT JOIN ORDERS AS o
               ON lo.CUSTOMERID = o.CUSTOMERID
                  AND lo.LatestOrderDate = o.ORDERDATE
             LEFT JOIN ORDERITEMS AS oi
               ON o.ORDERID = oi.ORDERID
             LEFT JOIN PRODUCTS AS p
               ON oi.PRODUCTID = p.PRODUCTID
             WHERE c.CUSTOMERID = @CustomerId
             ORDER BY 
               c.LASTNAME, 
               c.FIRSTNAME, 
               o.ORDERDATE DESC;
    END TRY
    BEGIN CATCH
      DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
      SELECT 
          @ErrorMessage = ERROR_MESSAGE(), 
          @ErrorSeverity = ERROR_SEVERITY(), 
          @ErrorState = ERROR_STATE();
      PRINT 'Error occurred: ' + @ErrorMessage;
      RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
  END;