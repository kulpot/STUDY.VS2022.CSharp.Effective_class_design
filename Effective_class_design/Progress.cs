using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Important information:
 * Possible issues
 * Usage
 * Compatibility
 * Possible improvements (So you dont forget)
 */

// If only have another code structure in this namespace if its closely related to the first and is small
// If you have defined many related code structures, consider putting them under there own namespace

namespace Effective_class_design
{
    // Consider using struct if the instance can hold data no longer than a typical reference (64bits)
    // and doesn't get passed around to much, and wont ever need to be derived.
    // Don't append the word class to the class name...
    // Access fields directly, unless the get set accessor does important work
    class Progress
    {
        // Mandatory data should always be sent throug the constructor, unless a designer element
        // Everything else can be a property to be set.
        // You may want to provide the user with constructor overloads with parameters accepting commonly set properties
        // Or properties that are best suited to be set during creation.
        // Only accept what you need.
        public Progress(float initialProgress)
        {
            _percent = initialProgress;
        }

        public Progress() { } // Default constructor

        // Put inside of class unless in a namespace more specific to progress
        // Lets the user know what is available
        public enum Report { UnTouched, JustStarted, BelowHalfway, AboveHalfway, AlmostFinished, Done };
        // May be directly accessed in a derived class
        protected float _percent;

        /// <summary>
        /// Pretend this is an intense method. The enumeration is not set a head of time,
        /// so we will not have it as a readonly property.
        /// </summary>
        public Report GetCurrentProgressReport() //intermediate level of abstraction
        {
            if(_percent == 0)
            {
                return Report.UnTouched;
            }
            else if(_percent > 0 &&  _percent < 0.25)
            {
                return Report.JustStarted;
            }
            else if(_percent > 0.25 &&  _percent < 0.5)
            {
                return Report.BelowHalfway;
            }
            else if(_percent > 0.5 &&  _percent < 0.75)
            {
                return Report.AlmostFinished;
            }
            else return Report.Done;
        }

        // Not necessary for effective usage but looks very nice when called
        public void Reset()
        {
            _percent = 0;
        }

        // Everyboldy loves the ToString method
        public override string ToString()
        {
            return ((int)(_percent * 100 + 0.5)).ToString() + "%";
        }

        /// <summary>
        /// Pretend this will be useful outside of the Progress class scope.
        /// But relative enough to be placed in this class
        /// </summary>
        public static uint ProgressToUint(float progress)
        {
            return (uint)(progress + 0.5);
        }

        // Keep properties in their own region at the bottom
        #region Properties
        /// <summary>
        /// Gets or set the percentage of progress from 0-1
        /// </summary>
        public float Percent // Lower level abstraction
        {
            get { return _percent; }
            set 
            {
                // Restrict input
                if (value < 0) _percent = 0;
                if(value >= 1)
                {
                    _percent = 1;
                    // Raise progress complete event here
                }
                else
                {
                    _percent = value;
                }

                if (UseWholeNumbers)
                {
                    _percent = (int)(_percent + 0.5);
                }
            }
        }

        /// <summary>
        /// Gets whether or not the progress indicates completion
        /// </summary>
        public bool IsComplete // High level abstraction // Avoid appending the properties category in the name Ex: Progress is complete the class name should described the property where the category that the properties within.
        {
            get { return (_percent == 1); }
        }

        // You cannot access the field directly but elegant
        public bool UseWholeNumbers { get; set; }

        // The above properties provide the programmer with different levels of abstraction
        // High level of abstraction, still retaining flexibility
        // <summary>
        // Level of Abstraction
        // public bool IsComplete // High level abstraction
        // public float Percent   // Lower level abstraction
        // public Report GetCurrentProgressReport() //intermediate level of abstraction
        // <summary>
        #endregion
    }
}
