CREATE PROCEDURE GetCustomerData
    @CustomerId NVARCHAR(50),
    @EmailId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 1 
        FROM CUSTOMERS
        WHERE CUSTOMERID = @CustomerId
          AND EMAIL = @EmailId;
    END TRY
    BEGIN CATCH
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

       
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
