//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RuleEngine
{
    public partial class Rule
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual int RuleSetId
        {
            get { return _ruleSetId; }
            set
            {
                if (_ruleSetId != value)
                {
                    if (tblRuleSet != null && tblRuleSet.Id != value)
                    {
                        tblRuleSet = null;
                    }
                    _ruleSetId = value;
                }
            }
        }
        private int _ruleSetId;
    
        public virtual int RuleType
        {
            get;
            set;
        }
    
        public virtual string LeftPropertyName
        {
            get;
            set;
        }
    
        public virtual string ComparisonOperator
        {
            get;
            set;
        }
    
        public virtual string RightPropertyName
        {
            get;
            set;
        }
    
        public virtual string ExpectedValue
        {
            get;
            set;
        }
    
        public virtual Nullable<int> BooleanConnector
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual RuleSet tblRuleSet
        {
            get { return _tblRuleSet; }
            set
            {
                if (!ReferenceEquals(_tblRuleSet, value))
                {
                    var previousValue = _tblRuleSet;
                    _tblRuleSet = value;
                    FixuptblRuleSet(previousValue);
                }
            }
        }
        private RuleSet _tblRuleSet;

        #endregion
        #region Association Fixup
    
        private void FixuptblRuleSet(RuleSet previousValue)
        {
            if (previousValue != null && previousValue.tblRules.Contains(this))
            {
                previousValue.tblRules.Remove(this);
            }
    
            if (tblRuleSet != null)
            {
                if (!tblRuleSet.tblRules.Contains(this))
                {
                    tblRuleSet.tblRules.Add(this);
                }
                if (RuleSetId != tblRuleSet.Id)
                {
                    RuleSetId = tblRuleSet.Id;
                }
            }
        }

        #endregion
    }
}
