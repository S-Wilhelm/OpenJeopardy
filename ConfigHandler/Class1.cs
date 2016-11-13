using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHandler
{
    public class Class1
    {
        /*
         * This DLL will contain all of the logic necessary for serializing and deserializing
         * settings and questions for the OpenJeopardy game.
         * 
         * 
         * TODO Check on methods of defining serializable datatypes
         *      May be able to use built-in attributes and let the framework do the heavy-lifting...
         * TODO Determine whether config file updates need any level of backwards-compat
         *      For instance, if a set of questions is edited in a newer build, should it
         *      provide the option to avoid adding any new features to the config, or
         *      should we just say 'sorry, but try to keep the program up-to-date'?
         * 
         * The data that we will want to track (can be expanded later):
         *      * Categories
         *          * Questions
         *      * Point values
         *      * Should category titles be shown?
         *      * Format version
         */
    }
}
