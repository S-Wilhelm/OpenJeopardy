using System;
using System.Collections.Generic;
using System.Linq;

using ConfigHandler;

namespace OpenJeopardy
{
    public class GameModel
    {
        // REFINE This class largely replicates the members of ConfigHandler.ConfigWrapper

        private GameModel()
        {
            
        }

        public static GameModel GenerateModel(ConfigWrapper config)
        {
            return new GameModel
            {
                Categories =
                    config.Categories.Select( category => ExtendedQuestionCategory.ExtendCategory(category, config.PointValues)).ToList()
            };
        }

        public List<ExtendedQuestionCategory> Categories { get; private set; }
    }

    public class ExtendedQuestionCategory : QuestionCategory<ExtendedQuestionEntry>
    {
        private ExtendedQuestionCategory()
        {
            
        }

        public static ExtendedQuestionCategory ExtendCategory(QuestionCategory<QuestionEntry> category, IEnumerable<Int32> points)
        {
            return new ExtendedQuestionCategory
            {
                Heading = category.Heading,
                QuestionEntries = category.QuestionEntries.Zip(points, ExtendedQuestionEntry.ExtendQuestion).ToList()
            };
        }
    }

    public class ExtendedQuestionEntry : QuestionEntry
    {
        private ExtendedQuestionEntry()
        {
            
        }

        public static ExtendedQuestionEntry ExtendQuestion(QuestionEntry question, Int32 points)
        {
            // TODO Research a way to extend the question without creating an entire new instance

            return new ExtendedQuestionEntry
            {
                Question = question.Question,
                Answer = question.Answer,
                PointValue = points
            };
        }

        public Int32 PointValue { get; private set; }
        public Boolean Answered { get; set; } = false;
    }
}