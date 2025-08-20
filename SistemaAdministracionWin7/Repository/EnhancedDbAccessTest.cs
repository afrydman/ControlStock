using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Persistence;
using Persistence.LogService;

namespace Repository
{
    /// <summary>
    /// Test class to verify Enhanced Database Access functionality
    /// Run this after implementing the enhanced database layer to ensure everything works correctly
    /// </summary>
    public static class EnhancedDbAccessTest
    {
        private static readonly DatabaseLogger _logger = LoggerFactory.GetDatabaseLogger("DatabaseTest");

        /// <summary>
        /// Run all tests to verify the enhanced database access layer
        /// </summary>
        public static void RunAllTests()
        {
            try
            {
                _logger.LogInfo("Starting Enhanced Database Access tests", "TEST_START");

                // Test 1: Parameter validation
                TestParameterValidation();

                // Test 2: Error handling
                TestErrorHandling();

                // Test 3: Performance logging
                TestPerformanceLogging();

                // Test 4: Connection logging
                TestConnectionLogging();

                _logger.LogInfo("All Enhanced Database Access tests completed successfully", "TEST_COMPLETE");
                Console.WriteLine("✅ All database access tests passed!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database access tests failed", ex, "TEST_FAILURE");
                Console.WriteLine($"❌ Database tests failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Test parameter validation functionality
        /// </summary>
        private static void TestParameterValidation()
        {
            _logger.LogInfo("Testing parameter validation", "TEST_PARAM_VALIDATION");

            var testRepo = new TestRepository();

            try
            {
                // This should throw ArgumentNullException
                testRepo.TestParameterValidation(null, "validValue");
                throw new Exception("Parameter validation test failed - should have thrown ArgumentNullException");
            }
            catch (ArgumentNullException)
            {
                // Expected behavior
                _logger.LogInfo("Parameter validation test passed", "TEST_PARAM_VALIDATION");
            }

            try
            {
                // This should throw ArgumentNullException for empty string
                testRepo.TestParameterValidation("", "validValue");
                throw new Exception("Parameter validation test failed - should have thrown ArgumentNullException for empty string");
            }
            catch (ArgumentNullException)
            {
                // Expected behavior
                _logger.LogInfo("Empty string validation test passed", "TEST_PARAM_VALIDATION");
            }

            // This should not throw
            testRepo.TestParameterValidation("validParam1", "validParam2");
            _logger.LogInfo("Valid parameters test passed", "TEST_PARAM_VALIDATION");
        }

        /// <summary>
        /// Test error handling functionality
        /// </summary>
        private static void TestErrorHandling()
        {
            _logger.LogInfo("Testing error handling", "TEST_ERROR_HANDLING");

            var testRepo = new TestRepository();

            try
            {
                // Test SQL exception handling
                testRepo.TestSqlExceptionHandling();
                throw new Exception("SQL exception test failed - should have thrown DatabaseOperationException");
            }
            catch (DatabaseOperationException ex)
            {
                // Verify the exception contains the expected information
                if (ex.SqlCommand != null && ex.Message.Contains("Test SQL Error"))
                {
                    _logger.LogInfo("SQL exception handling test passed", "TEST_ERROR_HANDLING");
                }
                else
                {
                    throw new Exception("SQL exception test failed - exception missing expected information");
                }
            }
        }

        /// <summary>
        /// Test performance logging functionality
        /// </summary>
        private static void TestPerformanceLogging()
        {
            _logger.LogInfo("Testing performance logging", "TEST_PERFORMANCE");

            var testRepo = new TestRepository();

            // Test normal performance (should not trigger warning)
            testRepo.TestNormalPerformance();

            // Test slow performance (should trigger warning)
            testRepo.TestSlowPerformance();

            _logger.LogInfo("Performance logging tests completed", "TEST_PERFORMANCE");
        }

        /// <summary>
        /// Test connection logging functionality
        /// </summary>
        private static void TestConnectionLogging()
        {
            _logger.LogInfo("Testing connection logging", "TEST_CONNECTION");

            // Test connection info logging
            var connectionString = "Server=test;Database=test;Trusted_Connection=true;";
            EnhancedDbAccess.LogConnectionInfo(connectionString, "Test Context");

            _logger.LogInfo("Connection logging test completed", "TEST_CONNECTION");
        }

        /// <summary>
        /// Test repository to verify enhanced database access functionality
        /// </summary>
        private class TestRepository : EnhancedDbRepository
        {
            public TestRepository() : base(true, "TestRepository")
            {
                // Use a test connection string or the default one
            }

            public override string DEFAULT_SELECT => "SELECT 1";

            public void TestParameterValidation(string param1, string param2)
            {
                ValidateParameters(
                    ("param1", param1),
                    ("param2", param2)
                );
            }

            public void TestSqlExceptionHandling()
            {
                // Simulate a SQL exception by attempting an invalid query
                var invalidSql = "SELECT * FROM NonExistentTable_Test_12345";
                
                try
                {
                    QueryFirstOrDefault<object>(invalidSql, null, "Test SQL Error");
                }
                catch (Exception ex)
                {
                    throw HandleDatabaseException(ex, "Test SQL Error", invalidSql);
                }
            }

            public void TestNormalPerformance()
            {
                // Simulate a fast operation
                LogDatabaseOperation("TEST_FAST", "Fast operation test", true, 100);
            }

            public void TestSlowPerformance()
            {
                // Simulate a slow operation
                LogSlowQuery("SlowTestQuery", 2500, 1000);
            }
        }

        /// <summary>
        /// Test connection string validation
        /// </summary>
        public static void TestConnectionString()
        {
            try
            {
                var testRepo = new TestRepository();
                _logger.LogInfo("Connection string test passed", "TEST_CONNECTION_STRING");
                Console.WriteLine("✅ Connection string test passed");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Connection string test failed", ex, "TEST_CONNECTION_STRING");
                Console.WriteLine($"❌ Connection string test failed: {ex.Message}");
                Console.WriteLine("Please verify your app.config connection strings are properly configured.");
            }
        }

        /// <summary>
        /// Test parameter creation helpers
        /// </summary>
        public static void TestParameterCreation()
        {
            var testRepo = new TestRepository();

            // Test individual parameter creation
            var param1 = testRepo.CreateParameter("@TestParam", "TestValue");
            if (param1.ParameterName != "@TestParam" || param1.Value.ToString() != "TestValue")
            {
                throw new Exception("Individual parameter creation test failed");
            }

            // Test null value handling
            var param2 = testRepo.CreateParameter("@NullParam", null);
            if (param2.Value != DBNull.Value)
            {
                throw new Exception("Null parameter handling test failed");
            }

            // Test parameters from anonymous object
            var paramList = testRepo.CreateParameters(new { Id = 123, Name = "Test", Active = true });
            if (paramList.Count != 3)
            {
                throw new Exception("Anonymous object parameter creation test failed");
            }

            _logger.LogInfo("Parameter creation tests passed", "TEST_PARAM_CREATION");
            Console.WriteLine("✅ Parameter creation tests passed");
        }
    }
}