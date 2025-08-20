using System;
using System.Diagnostics;
using System.Windows.Forms;
using Persistence.LogService;

namespace SharedForms
{
    /// <summary>
    /// Base form class with enhanced logging capabilities (.NET Framework 4.0 compatible)
    /// </summary>
    public partial class LoggedForm : Form
    {
        protected readonly UILogger _logger;
        protected readonly string _formName;
        private readonly Stopwatch _formLoadTimer;
        private bool _isLoaded = false;

        public LoggedForm() : this(null)
        {
        }

        public LoggedForm(string formName)
        {
            _formName = formName ?? this.GetType().Name;
            _logger = LoggerFactory.GetUILogger(_formName);
            _formLoadTimer = Stopwatch.StartNew();
            
            // Set up form event handlers
            this.Load += LoggedForm_Load;
            this.FormClosed += LoggedForm_FormClosed;
            this.Shown += LoggedForm_Shown;
        }

        protected void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        private void LoggedForm_Load(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInfo(string.Format("Form loading: {0}", _formName), LogOperations.FORM_LOAD);
                
                // Call the overridable load method
                OnFormLoading();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error during form load: {0}", _formName), ex, LogOperations.FORM_LOAD);
                throw;
            }
        }

        private void LoggedForm_Shown(object sender, EventArgs e)
        {
            _formLoadTimer.Stop();
            _isLoaded = true;
            
            _logger.LogFormLoad(_formName, _formLoadTimer.ElapsedMilliseconds, true);
            
            // Log slow form loads
            if (_formLoadTimer.ElapsedMilliseconds > 3000)
            {
                _logger.LogWarning(string.Format("Slow form load detected: {0}", _formName), "UI_PERFORMANCE");
            }
        }

        private void LoggedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logger.LogInfo(string.Format("Form closed: {0} (Reason: {1})", _formName, e.CloseReason), "FORM_CLOSE");
        }

        // Virtual methods for derived forms to override
        protected virtual void OnFormLoading()
        {
            // Override in derived classes
        }

        // Logging helper methods for UI events
        protected void LogUserAction(string action, Control control, string value)
        {
            var controlName = control != null ? control.Name : "Unknown";
            _logger.LogUserAction(action, _formName, controlName, value);
        }

        protected void LogUserAction(string action)
        {
            LogUserAction(action, null, null);
        }

        protected void LogValidationError(string fieldName, string errorMessage)
        {
            _logger.LogValidationError(fieldName, errorMessage, _formName);
        }

        protected void LogButtonClick(string buttonName, string additionalInfo)
        {
            LogUserAction("Button Click", null, string.Format("{0}: {1}", buttonName, additionalInfo ?? ""));
        }

        protected void LogButtonClick(string buttonName)
        {
            LogButtonClick(buttonName, null);
        }

        protected void LogTextChanged(string controlName, string newValue)
        {
            // Only log significant text changes to avoid spam
            if (!string.IsNullOrWhiteSpace(newValue))
            {
                LogUserAction("Text Changed", null, string.Format("{0}: {1} chars", controlName, newValue.Length));
            }
        }

        protected void LogSelectionChanged(string controlName, string selectedValue)
        {
            LogUserAction("Selection Changed", null, string.Format("{0}: {1}", controlName, selectedValue));
        }

        protected void LogDataLoad(string dataType, int recordCount, long loadTimeMs)
        {
            _logger.LogInfo(string.Format("Data loaded: {0} | {1} records in {2}ms", dataType, recordCount, loadTimeMs), "DATA_LOAD");
            
            if (loadTimeMs > 2000)
            {
                _logger.LogWarning(string.Format("Slow data load: {0} took {1}ms", dataType, loadTimeMs), "DATA_PERFORMANCE");
            }
        }

        protected void LogDataSave(string dataType, bool success, string errorMessage)
        {
            if (success)
            {
                _logger.LogInfo(string.Format("Data saved successfully: {0}", dataType), "DATA_SAVE");
            }
            else
            {
                _logger.LogError(string.Format("Data save failed: {0} | {1}", dataType, errorMessage ?? "Unknown error"), null, "DATA_SAVE");
            }
        }

        protected void LogDataSave(string dataType, bool success)
        {
            LogDataSave(dataType, success, null);
        }

        protected TResult LoggedDataOperation<TResult>(Func<TResult> operation, string operationName, TResult defaultValue)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogDebug(string.Format("Starting data operation: {0}", operationName), operationName);
                
                var result = operation();
                
                stopwatch.Stop();
                _logger.LogInfo(string.Format("Data operation completed: {0} in {1}ms", operationName, stopwatch.ElapsedMilliseconds), operationName);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(string.Format("Data operation failed: {0} after {1}ms", operationName, stopwatch.ElapsedMilliseconds), ex, operationName);
                
                // Show user-friendly error message
                ShowErrorMessage(string.Format("Operation failed: {0}", operationName), ex.Message);
                
                return defaultValue;
            }
        }

        protected TResult LoggedDataOperation<TResult>(Func<TResult> operation, string operationName)
        {
            return LoggedDataOperation(operation, operationName, default(TResult));
        }

        protected void LoggedDataOperation(Action operation, string operationName)
        {
            LoggedDataOperation(() => { operation(); return true; }, operationName);
        }

        // User-friendly error handling
        protected void ShowErrorMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            _logger.LogInfo(string.Format("Error message shown to user: {0} - {1}", title, message), "USER_MESSAGE");
        }

        protected void ShowWarningMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _logger.LogInfo(string.Format("Warning message shown to user: {0} - {1}", title, message), "USER_MESSAGE");
        }

        protected void ShowInfoMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            _logger.LogDebug(string.Format("Info message shown to user: {0} - {1}", title, message), "USER_MESSAGE");
        }

        // Form-specific validation with logging
        protected bool ValidateField(Control control, Func<string, bool> validator, string errorMessage)
        {
            try
            {
                var value = control.Text;
                var isValid = validator(value);
                
                if (!isValid)
                {
                    LogValidationError(control.Name, errorMessage);
                    
                    // Highlight the control
                    control.BackColor = System.Drawing.Color.LightCoral;
                    control.Focus();
                    
                    ShowWarningMessage("Validation Error", errorMessage);
                }
                else
                {
                    // Reset control appearance
                    control.BackColor = System.Drawing.SystemColors.Window;
                }
                
                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Validation error for {0}", control.Name), ex, LogOperations.VALIDATION);
                return false;
            }
        }

        protected bool ValidateForm()
        {
            var isValid = true;
            
            try
            {
                // Call virtual method for specific validation
                isValid = OnValidateForm();
                
                if (isValid)
                {
                    _logger.LogInfo(string.Format("Form validation passed: {0}", _formName), LogOperations.VALIDATION);
                }
                else
                {
                    _logger.LogWarning(string.Format("Form validation failed: {0}", _formName), LogOperations.VALIDATION);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Form validation error: {0}", _formName), ex, LogOperations.VALIDATION);
                isValid = false;
            }
            
            return isValid;
        }

        // Virtual method for form-specific validation
        protected virtual bool OnValidateForm()
        {
            return true; // Override in derived classes
        }

        // Report generation with logging
        protected void GenerateReport(string reportName, Func<byte[]> reportGenerator)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInfo(string.Format("Starting report generation: {0}", reportName), LogOperations.REPORT_GENERATE);
                
                var reportData = reportGenerator();
                
                stopwatch.Stop();
                _logger.LogReportGeneration(reportName, 1, stopwatch.ElapsedMilliseconds, true);
                
                // Handle report display/save logic here
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(string.Format("Report generation failed: {0}", reportName), ex, LogOperations.REPORT_GENERATE);
                ShowErrorMessage("Report Error", string.Format("Failed to generate {0}: {1}", reportName, ex.Message));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_isLoaded)
                {
                    _logger.LogInfo(string.Format("Form disposed: {0}", _formName), "FORM_DISPOSE");
                }
                
                if (_formLoadTimer != null)
                {
                    _formLoadTimer.Stop();
                }
            }
            
            base.Dispose(disposing);
        }
    }
}