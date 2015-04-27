﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace ebis.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class dbFinance : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new dbFinance object using the connection string found in the 'dbFinance' section of the application configuration file.
        /// </summary>
        public dbFinance() : base("name=dbFinance", "dbFinance")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbFinance object.
        /// </summary>
        public dbFinance(string connectionString) : base(connectionString, "dbFinance")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbFinance object.
        /// </summary>
        public dbFinance(EntityConnection connection) : base(connection, "dbFinance")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<naklady> naklady
        {
            get
            {
                if ((_naklady == null))
                {
                    _naklady = base.CreateObjectSet<naklady>("naklady");
                }
                return _naklady;
            }
        }
        private ObjectSet<naklady> _naklady;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the naklady EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTonaklady(naklady naklady)
        {
            base.AddObject("naklady", naklady);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="dbFinanceModel", Name="naklady")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class naklady : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new naklady object.
        /// </summary>
        /// <param name="pk_id">Initial value of the pk_id property.</param>
        /// <param name="jmeno">Initial value of the jmeno property.</param>
        /// <param name="typ">Initial value of the typ property.</param>
        public static naklady Createnaklady(global::System.Int32 pk_id, global::System.String jmeno, global::System.Int32 typ)
        {
            naklady naklady = new naklady();
            naklady.pk_id = pk_id;
            naklady.jmeno = jmeno;
            naklady.typ = typ;
            return naklady;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 pk_id
        {
            get
            {
                return _pk_id;
            }
            set
            {
                if (_pk_id != value)
                {
                    Onpk_idChanging(value);
                    ReportPropertyChanging("pk_id");
                    _pk_id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("pk_id");
                    Onpk_idChanged();
                }
            }
        }
        private global::System.Int32 _pk_id;
        partial void Onpk_idChanging(global::System.Int32 value);
        partial void Onpk_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String jmeno
        {
            get
            {
                return _jmeno;
            }
            set
            {
                OnjmenoChanging(value);
                ReportPropertyChanging("jmeno");
                _jmeno = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("jmeno");
                OnjmenoChanged();
            }
        }
        private global::System.String _jmeno;
        partial void OnjmenoChanging(global::System.String value);
        partial void OnjmenoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 typ
        {
            get
            {
                return _typ;
            }
            set
            {
                OntypChanging(value);
                ReportPropertyChanging("typ");
                _typ = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("typ");
                OntypChanged();
            }
        }
        private global::System.Int32 _typ;
        partial void OntypChanging(global::System.Int32 value);
        partial void OntypChanged();

        #endregion

    
    }

    #endregion

    
}
