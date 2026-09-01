Public Module Strings
    ' Constants for various string messages/user prompts used in the application

    ' Application strings.
    Public Const STR_TITLE_DEFAULT As String = "Hale-MRI"
    Public Const STR_TITLE_APPLICATION_ERROR As String = "Application Error"
    Public Const STR_TITLE_ENCODER_ERROR As String = "Encoder Error"
    Public Const STR_TITLE_DATABASE_ERROR As String = "Database Error"
    Public Const STR_TITLE_DATABASE_SETUP As String = "Database Setup"
    Public Const STR_TITLE_SCANDATA_SELECT As String = "Select Scan Data File"

    Public Const STR_DATABASE_CONNECTION_PARAMS As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}"

    ' Application Objects.
    Public Const STR_OBJECT_CALIBRATION As String = "Calibration"
    Public Const STR_OBJECT_CUSTOMER As String = "Customer"
    Public Const STR_OBJECT_JOB As String = "Job"
    Public Const STR_OBJECT_VESSEL As String = "Vessel"
    Public Const STR_OBJECT_JOBDETAIL As String = "Job Detail"
    Public Const STR_OBJECT_REPORT As String = "Report"
    Public Const STR_OBJECT_MANUFACTURER As String = "Manufacturer"
    Public Const STR_OBJECT_PROPELLER As String = "Propeller"
    Public Const STR_OBJECT_MEASUREMENT As String = "Measurement"
    Public Const STR_OBJECT_SETTING As String = "Setting"

    Public Const STR_FORM_CUSTOMERS As String = "FrmCustomers"
    Public Const STR_FORM_JOBS As String = "FrmJobs"
    Public Const STR_FORM_VESSELS As String = "FrmVessels"
    Public Const STR_FORM_MEASUREMENTS As String = "FrmMeasurements"
    Public Const STR_FORM_REPORTS As String = "FrmReports"
    Public Const STR_FORM_CALIBRATION As String = "FrmCalibration"
    Public Const STR_FORM_COMPARISON As String = "FrmComparison"
    Public Const STR_FORM_GRAPH As String = "FrmGraph"
    Public Const STR_FORM_INSPECT As String = "FrmInspect"
    Public Const STR_FORM_MANUFACTURERS As String = "FrmManufacturers"
    Public Const STR_FORM_PROPELLERS As String = "FrmPropellers"
    Public Const STR_FORM_SETTINGS As String = "FrmSettings"
    Public Const STR_FORM_STARTUP As String = "FrmHaleMRI"

    ' Error message strings.
    Public Const STR_ERR_ADDNEW As String = "Error adding new {0}: {1}"
    Public Const STR_ERR_APPLICATION_LOAD As String = "Error loading the application {0}: "
    Public Const STR_ERR_BAD_OR_MISSING_REQUIRED_FIELD As String = "All required fields, shown in red, must be completed and the record saved before continuing."
    Public Const STR_ERR_DATABASE_NOT_FOUND As String = "The database file {0} was not found in that folder."
    Public Const STR_ERR_ENCODERS As String = "No encoders found"
    Public Const STR_ERR_ENCODERS_DETAILS As String = STR_ERR_ENCODERS & ". You will not be able to take any measurements until the encoder hardware is properly configured, connected and powered on. ({0})"
    Public Const STR_ERR_FILE_NOT_FOUND As String = "File not found: {0}"
    Public Const STR_ERR_FILE_OPEN As String = "Error opening the {0} file: {1}"
    Public Const STR_ERR_FILTER As String = "Error filtering: {0}"
    Public Const STR_ERR_FORM_OPEN As String = "Error opening the {0} form: {1}"
    Public Const STR_ERR_INVALID_SELECTION As String = "The selected item is not in the list. Please select a valid item."
    Public Const STR_ERR_LOGIN As String = "Login error: {0}"
    Public Const STR_ERR_JOB_SELECT As String = "Error selecting a job: {0}"
    Public Const STR_ERR_NAVIGATION As String = "Navigation error: {0}"
    Public Const STR_ERR_NO_DEFAULT_VALUE As String = "Error no default value: {0}"
    Public Const STR_ERR_RECORD_SELECT As String = "Error moving to the selected record: {0}"
    Public Const STR_ERR_OBJECT_LOAD As String = "Error loading the {0}: {1}"
    Public Const STR_ERR_SCANDATA_EXPORT As String = "Error exporting scan data: {0}"
    Public Const STR_ERR_SCANDATA_IMPORT As String = "Error importing scan data: {0}"
    Public Const STR_ERR_SCANDATA_SELECT As String = "Error selecting scan data: {0}"
    Public Const STR_ERR_SCANDATA_TEXT As String = "No job was created from the scan data file because it is corrupted or missing required data."
    Public Const STR_ERR_SELECTION_REQUIRED As String = "Please select a {0} from the list or enter a new one."

    ' User prompt strings.
    Public Const STR_PROMPT_DELETE As String = "Are you sure you want to delete the selected {0}?"
    Public Const STR_PROMPT_DELETE_ALL As String = "Are you sure you want to delete all {0}?"
    Public Const STR_PROMPT_DATABASE_CONNECTION As String = "Database connection not found." & vbCrLf & vbCrLf &
          "Before the first run, you must select the application database folder?"
    Public Const STR_PROMPT_PICK_FOLDER As String = "Select the folder containing {0}"
    Public Const STR_PROMPT_PICK_FILE As String = "Select the {0} file"
    Public Const STR_PROMPT_REMOVE As String = "Are you sure you want to delete {0} '{1}'?"

    ' Encoder strings.
    Public Const STR_ERR_HARDWARE_INIT As String = "Encoder initialization error."
    Public Const STR_ERR_COUNT As String = "Encoder count error."
    Public Const STR_ERR_ENCODER_INVALID As String = "Invalid encoder number."
    Public Const STR_ERR_CALIBRATION_READ As String = "Error retrieving calibration data from the database: "
    Public Const STR_ERR_CALIBRATION_WRITE As String = "Error saving calibration data to the database: "
    Public Const STR_ERR_EXPORT As String = "Error exporting calibration data: "
    Public Const STR_ERR_IMPORT As String = "Error importing calibration data: "

    Public Const STR_CALIBRATION_DEFAULT As String = "Default"
    Public Const STR_STATUS_BUSY As String = "Busy"
    Public Const STR_STATUS_ERROR As String = "Encoder Error"
    Public Const STR_STATUS_NO_ENCODERS As String = "No Encoders"
    Public Const STR_STATUS_NOT_INITIALIZED As String = "Not Initialized"
    Public Const STR_STATUS_READY As String = "Ready"

    Public Const STR_PROMPT_UNSAVED_CHANGES As String = "There are unsaved changes. Do you want to save them now?"

    ' String parameters for functions.
    Public Const STR_PARAM_DECIMAL_PLACES As String = "F2"  ' This is a ~Settings parameter in dB.

    ' Setting names used in My.Settings.
    Public Const STR_SETTING_COMPANY_NAME As String = "CompanyName"
    Public Const STR_SETTING_COMPANY_ADDRESS As String = "CompanyAddress"
    Public Const STR_SETTING_COMPANY_PHONE As String = "CompanyPhone"
    Public Const STR_SETTING_COMPANY_CONTACT As String = "CompanyContact"
    Public Const STR_SETTING_COMPANY_EMAIL As String = "CompanyEmail"
    Public Const STR_SETTING_COMPANY_WEBSITE As String = "CompanyWebsite"
    Public Const STR_SETTING_APPLICATION_DEFAULT_FOLDER As String = "ApplicationDefaultFolder"
    Public Const STR_SETTING_DATABASE_CONNECTION_STRING As String = "DbConnectionString"
    Public Const STR_SETTING_ENCODER_DATA_DEFAULT_FOLDER As String = "EncoderDataDefaultFolder"
    Public Const STR_SETTING_ENCODER_DEFAULT_SAMPLE_PERIOD As String = "EncoderDefaultSampleInterval"
    Public Const STR_SETTING_ENCODER_MAX_SAMPLES_PER_SCAN As String = "EncoderMaxSamplesPerScan"
    Public Const STR_SETTING_FORMAT_LOGERROR As String = "{0:yyyy-MM-dd HH:mm:ss.fff} [ERROR] {1}{2}StackTrace:{3}{4}{5}"
    Public Const STR_SETTING_FORMAT_LOGINFO As String = "{0:yyyy-MM-dd HH:mm:ss.fff} [INFO] {1}{2}"
    Public Const STR_SETTING_NAME_LOGDIR As String = "logs"
    Public Const STR_SETTING_NAME_LOGFILE As String = "Hale-MRI.log"
    Public Const STR_SETTING_NAME_DBFILE As String = "HaleMRI.accdb"

    ' File dialog filter strings.
    Public Const STR_DIALOG_FILTER_ALL As String = "All Files (*.*)|*.*"
    Public Const STR_DIALOG_FILTER_CSV As String = "CSV Files |*.csv;*.txt|All Files (*.*)|*.*"
    Public Const STR_DIALOG_FILTER_DATABASE As String = "Database Files|*.mdb;*.accdb;*.sqlite;*.db|All Files (*.*)|*.*"
    Public Const STR_DIALOG_FILTER_IMAGE As String = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*"
    Public Const STR_DIALOG_FILTER_SCANDATA As String = "ScanData Files (*.txt)|*.txt|All Files (*.*)|*.*"
    Public Const STR_DIALOG_FILTER_TEXT As String = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
    Public Const STR_DIALOG_DELETE_ROW As String = "Delete {0} '{1}'?"
    Public Const STR_DIALOG_DELETE_ROWS As String = "Delete the {0} selected {1}?"
    Public Const STR_DIALOG_PROMPT_DB_SELECT As String = "Select A Database File"
    Public Const STR_DIALOG_PROMPT_IMAGE_SELECT As String = "Select An Image File"
    Public Const STR_DIALOG_PROMPT_NEW_CUSTOMER_VESSEL As String = "{0} {1} {2} {3} not found. Do you want to add them to the database?"
End Module
