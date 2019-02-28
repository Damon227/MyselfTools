using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.Foundation.DynamicLinq
{
    public class DynamicPredicate
    {
        public string Header { get; set; }

        public string Prefix { get; set; }

        public string Key { get; set; }

        public string Operator { get; set; }

        public object Value { get; set; }

        public string ValueType { get; set; }

        public string Footer { get; set; }
    }

    public static class DynamicPredicateExtensions
    {
        [SuppressMessage("ReSharper", "ConvertIfStatementToConditionalTernaryExpression")]
        public static string ToQueryablePredicate(this List<DynamicPredicate> dynamicPredicate)
        {
            if (dynamicPredicate?.Count > 0)
            {
                string whereString = string.Empty;

                foreach (DynamicPredicate predicate in dynamicPredicate)
                {
                    string predicateValue;
                    if (predicate.ValueType == typeof(string).ToString())
                    {
                        if (predicate.Value.ToString().ToUpper() == "NULL")
                        {
                            predicateValue = predicate.Value.ToString();
                        }
                        else
                        {
                            predicateValue = $"\"{predicate.Value}\"";
                        }
                    }
                    else if (predicate.ValueType == typeof(DateTimeOffset).ToString())
                    {
                        string time = predicate.Value.ToString();
                        time = time.Contains("+") ? time : time + " +08:00";
                        predicateValue = $"DateTimeOffset.Parse(\"{time}\")";
                    }
                    else
                    {
                        predicateValue = $"{predicate.Value}";
                    }

                    string operatorString;
                    if (predicate.Operator.Contains("Contains"))
                    {
                        operatorString = $".Contains({predicateValue})";
                    }
                    else if (predicate.Operator.Contains("StartWith"))
                    {
                        operatorString = $".StartWith({predicateValue})";
                    }
                    else if (predicate.Operator.Contains("EndWith"))
                    {
                        operatorString = $".EndWith({predicateValue})";
                    }
                    else
                    {
                        operatorString = $"{predicate.Operator} {predicateValue}";
                    }

                    whereString += $@"{predicate.Header} {predicate.Key}{operatorString} {predicate.Footer}";
                }

                return whereString;
            }

            return null;
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToConditionalTernaryExpression")]
        public static string ToSqlPredicate(this List<DynamicPredicate> dynamicPredicate)
        {
            if (dynamicPredicate?.Count > 0)
            {
                string whereString = string.Empty;

                foreach (DynamicPredicate predicate in dynamicPredicate)
                {
                    string predicateValue;
                    if (predicate.ValueType == typeof(string).ToString())
                    {
                        if (predicate.Value.ToString().ToUpper() == "NULL")
                        {
                            predicateValue = "''";
                        }
                        else
                        {
                            predicateValue = $"'{predicate.Value}'";
                        }
                    }
                    else if (predicate.ValueType == typeof(DateTimeOffset).ToString())
                    {
                        string time = predicate.Value.ToString();
                        time = time.Contains("+") ? time : time + " +08:00";
                        predicateValue = $"'{DateTimeOffset.Parse(time)}'";
                    }
                    else if (predicate.ValueType == typeof(bool).ToString())
                    {
                        predicateValue = (bool)predicate.Value ? "1" : "0";
                    }
                    else
                    {
                        predicateValue = $"{predicate.Value}";
                    }

                    string operatorString;
                    if (predicate.Operator.Contains("Contains"))
                    {
                        operatorString = $" LIKE '%{predicate.Value}%' ";
                    }
                    else if (predicate.Operator.Contains("StartWith"))
                    {
                        operatorString = $" LIKE '{predicate.Value}%' ";
                    }
                    else if (predicate.Operator.Contains("EndWith"))
                    {
                        operatorString = $" LIKE '%{predicate.Value}' ";
                    }
                    else
                    {
                        operatorString = $"{predicate.Operator} {predicateValue}";
                    }

                    if (string.IsNullOrEmpty(predicate.Prefix))
                    {
                        whereString += $@"{predicate.Header} {predicate.Key}{operatorString} {predicate.Footer}";
                    }
                    else
                    {
                        whereString += $@"{predicate.Header} {predicate.Prefix}.{predicate.Key}{operatorString} {predicate.Footer}";
                    }
                }

                return whereString;
            }

            return null;
        }
    }
}