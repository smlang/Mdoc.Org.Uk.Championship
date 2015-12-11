using System;
using System.Collections.Specialized;

namespace Mdoc.Org.Uk.Championship.Library
{

	/// <summary>
	/// Interface between the UI Layer and the Business Layer
	/// </summary>
	public interface IUserInteraction {

        Int32 GetChoice(Int32 defaultValue, String prompt, params String[] options);
        String GetValue(String prompt, String defaultValue);

        /// <summary>
        /// UI to display a information message
        /// </summary>
        void ShowInformation(String message);

        /// <summary>
        /// UI to display a warning message
        /// </summary>
        void ShowWarning(String message);
        
        /// <summary>
		/// UI to display an error message
		/// </summary>
        void ShowError(String message);
	}
}
