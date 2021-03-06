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
    public partial class StatusTransition
    {
        #region Primitive Properties
    
        public virtual int VehicleStatusId
        {
            get;
            set;
        }
    
        public virtual int OrderNo
        {
            get;
            set;
        }
    
        public virtual int TransitionSpecificationId
        {
            get { return _transitionSpecificationId; }
            set
            {
                if (_transitionSpecificationId != value)
                {
                    if (tblTransitionSpecification != null && tblTransitionSpecification.Id != value)
                    {
                        tblTransitionSpecification = null;
                    }
                    _transitionSpecificationId = value;
                }
            }
        }
        private int _transitionSpecificationId;
    
        public virtual int VehicleStatusTransitionId
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual TransitionSpecification tblTransitionSpecification
        {
            get { return _tblTransitionSpecification; }
            set
            {
                if (!ReferenceEquals(_tblTransitionSpecification, value))
                {
                    var previousValue = _tblTransitionSpecification;
                    _tblTransitionSpecification = value;
                    FixuptblTransitionSpecification(previousValue);
                }
            }
        }
        private TransitionSpecification _tblTransitionSpecification;

        #endregion
        #region Association Fixup
    
        private void FixuptblTransitionSpecification(TransitionSpecification previousValue)
        {
            if (previousValue != null && previousValue.tblStatusTransitions.Contains(this))
            {
                previousValue.tblStatusTransitions.Remove(this);
            }
    
            if (tblTransitionSpecification != null)
            {
                if (!tblTransitionSpecification.tblStatusTransitions.Contains(this))
                {
                    tblTransitionSpecification.tblStatusTransitions.Add(this);
                }
                if (TransitionSpecificationId != tblTransitionSpecification.Id)
                {
                    TransitionSpecificationId = tblTransitionSpecification.Id;
                }
            }
        }

        #endregion
    }
}
