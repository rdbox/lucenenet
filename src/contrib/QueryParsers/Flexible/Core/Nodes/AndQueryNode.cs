﻿using Lucene.Net.QueryParsers.Flexible.Core.Parser;
using Lucene.Net.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucene.Net.QueryParsers.Flexible.Core.Nodes
{
    public class AndQueryNode : BooleanQueryNode
    {
        public AndQueryNode(IList<IQueryNode> clauses)
            : base(clauses)
        {
            if ((clauses == null) || (clauses.Count == 0))
            {
                throw new ArgumentException(
                    "AND query must have at least one clause");
            }
        }

        public override string ToString()
        {
            if (Children == null || Children.Count == 0)
                return "<boolean operation='and'/>";
            StringBuilder sb = new StringBuilder();
            sb.Append("<boolean operation='and'>");
            foreach (IQueryNode child in Children)
            {
                sb.Append("\n");
                sb.Append(child.ToString());

            }
            sb.Append("\n</boolean>");
            return sb.ToString();
        }

        public override ICharSequence ToQueryString(IEscapeQuerySyntax escapeSyntaxParser)
        {
            if (Children == null || Children.Count == 0)
                return new StringCharSequenceWrapper("");

            StringBuilder sb = new StringBuilder();
            String filler = "";
            foreach (IQueryNode child in Children)
            {
                sb.Append(filler).Append(child.ToQueryString(escapeSyntaxParser));
                filler = " AND ";
            }

            // in case is root or the parent is a group node avoid parenthesis
            if ((Parent != null && Parent is GroupQueryNode)
                || IsRoot)
                return new StringCharSequenceWrapper(sb.ToString());
            else
                return new StringCharSequenceWrapper("( " + sb.ToString() + " )");
        }


    }
}