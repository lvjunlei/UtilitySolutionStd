000000000}|0,0|False                                                    �[o3ܡ�                                                UtilityStd.git��                                                �S��7��M�������                                   ���[o3ܡ�                                                         �g@name         �\@totalSamples        �\@selfSamples        �\@moduleName                                       tF : \ W o r k \ G i t H u b \ U t i l i t y S o l u t i o n S t00000000000}|0,0|Falsek   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\Class1.cs|{00000000-0000-0000-0000-000000000000}|0,0|Falsex   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\AbstractTypeBuilder.cs|{00000000-0000-0000-0000-000000000000}|0,0|False|   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\AST\CompilationContext.cs|{00000000-0000-0000-0000-000000000000}|9,15|False�   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\AST\Helpers\AstBuildHelper.cs|{00000000-0000-0000-0000-000000000000}|158,49|False�   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\AST\Nodes\AstExceptionHandlingBlock.cs|{00000000-0000-0000-0000-000000000000}|11,8|False�   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\EmitInvoker\Delegates\DelegateInvoker.cs|{00000000-0000-0000-0000-000000000000}|83,22|False}   F:\Work\GitHub\UtilitySolutionStd\Utility.Mapper\DynamicAssemblyManager.cs|{00000000-0000-0000-0000-000000000000}|37,49|Falsem   F:\Work\GitHub\UtilitySolutionStd\EmitMaperTest\Program.cs|{00000000-0000-0000-0000-000000000000}|20,12|False                                                  �[o3ܡ�                                                UtilityStd.git��                                                �S��7��M�������                                   ���[o3ܡ�                                                         �g@name         �\@totalSamples        �\@selfSamples        �\@moduleName                                       tF : \ W o r k \ G i t H u b \ U t i l i t y S o l u t i o n S t d \ E m i t M a p e r T e s t \ P r o g r a m . c s !�-��!]��S��    ����������������                                       �㡨���[o3ܡ�                                                �S��7��M�������                                   ���[o3ܡ�                                                �㡨���[o3ܡ�                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ;
                        break;
                    case ExpressionType.Equal:
                        sb.Append(" =");
                        break;
                    default: break;
                }

                CreateUpdateBody(tableAlias, binaryExpression.Right, ref sb, ref sp);
            }

            if (expression is ConstantExpression constantExpression)
            {
                var parmName = $"param_{sp.Count}";
                sp.Add(new SqlParameter(parmName, constantExpression.Value));
                sb.Append($" @{parmName}");
            }

            if (expression is MemberExpression memberExpression)
            {
                sb.Append($"[{tableAlias}].[{memberExpression.Member.Name}]");
            }
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static (string, string) GetBatchSql<T>(IQueryable<T> query) where T : class, new()
        {
            string sqlQuery = query.ToSql();
            string tableAlias = sqlQuery.Substring(8, sqlQuery.IndexOf("]") - 8);
            int indexFROM = sqlQuery.IndexOf(Environment.NewLine);
            string sql = sqlQuery.Substring(indexFROM, sqlQuery.Length - indexFROM);
            sql = sql.Contains("{") ? sql.Replace("{", "{{") : sql; // Curly brackets have to escaped:
            sql = sql.Contains("}") ? sql.Replace("}", "}}") : sql; // https://github.com/aspnet/EntityFrameworkCore/issues/8820
            return (sql, tableAlias);
        }
    }
}
