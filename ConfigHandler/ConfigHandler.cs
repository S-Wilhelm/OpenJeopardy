using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConfigHandler
{
    public class ConfigHandler
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
         */

        // TODO Compare the two methods of deserializing a file
        public ConfigWrapper DeserializeBuffered(string configFile)
        {
            return JsonConvert.DeserializeObject<ConfigWrapper>(File.ReadAllText(configFile));
        }

        public ConfigWrapper DeserializeStream(string configFile)
        {
            ConfigWrapper parsed;

            using (StreamReader file = File.OpenText(configFile))
            {
                parsed = (ConfigWrapper) new JsonSerializer().Deserialize(file, typeof(ConfigWrapper));
            }

            return parsed;
        }

        // TODO Compare the two methods of serializing a file
        public void SerializeBuffered(string configFile, ConfigWrapper config)
        {
            File.WriteAllText(configFile, JsonConvert.SerializeObject(config));
        }

        public void SerializeStream(string configFile, ConfigWrapper config)
        {
            using (StreamWriter file = File.CreateText(configFile))
            {
                new JsonSerializer().Serialize(file, config);
            }
        }

        public const int DEFAULT_CATEGORY_COUNT = 6;
        public const int DEFAULT_ROW_COUNT = 5;
    }

    public class ConfigWrapper
    {
        /*
         * Data to track (can be expanded later):
         *      * Format version
         *      * Point values
         *      * Categories
         *          * Questions
         *
         * Potential enhancements:
         *      * Should category titles be shown?
         */

        public int FormatRevision { get; set; } = 1;
        public int[] PointValues { get; set; } = { 200, 400, 600, 800, 1000};

        public List<QuestionCategory> Categories { get; set; } =
            new List<QuestionCategory>(ConfigHandler.DEFAULT_CATEGORY_COUNT);
    }

    public class QuestionCategory
    {
        public string Heading { get; set; }

        /* TODO Evaluate the use of a list on a serializable type
         * This may cause unnecessary object instantiation, and it is normally advised
         * to avoid exposing reference types (e.g. collections) as settable properties
         */

        public List<QuestionEntry> QuestionEntries { get; set; } = new List<QuestionEntry>(ConfigHandler.DEFAULT_ROW_COUNT);
    }

    // REFINE Should this be a struct instead of a class?
    // Likely depends on the implementation of A/V element support...
    public class QuestionEntry
    {
        /* At present, this class is superfluous;
         * however, I want to allow the use of embedded
         * audio/video elements in a future revision.
         * The use of a custom type helps to prepare for
         * that functionality.
         */

        public string Question { get; set; }
    }
}