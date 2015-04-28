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
    public partial class dbProdList : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new dbProdList object using the connection string found in the 'dbProdList' section of the application configuration file.
        /// </summary>
        public dbProdList() : base("name=dbProdList", "dbProdList")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbProdList object.
        /// </summary>
        public dbProdList(string connectionString) : base(connectionString, "dbProdList")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbProdList object.
        /// </summary>
        public dbProdList(EntityConnection connection) : base(connection, "dbProdList")
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
        public ObjectSet<produkcni_listy> produkcni_listy
        {
            get
            {
                if ((_produkcni_listy == null))
                {
                    _produkcni_listy = base.CreateObjectSet<produkcni_listy>("produkcni_listy");
                }
                return _produkcni_listy;
            }
        }
        private ObjectSet<produkcni_listy> _produkcni_listy;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the produkcni_listy EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToprodukcni_listy(produkcni_listy produkcni_listy)
        {
            base.AddObject("produkcni_listy", produkcni_listy);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="dbProdListModel", Name="produkcni_listy")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class produkcni_listy : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new produkcni_listy object.
        /// </summary>
        /// <param name="pk_id">Initial value of the pk_id property.</param>
        /// <param name="jmeno_aktivity">Initial value of the jmeno_aktivity property.</param>
        public static produkcni_listy Createprodukcni_listy(global::System.Int32 pk_id, global::System.String jmeno_aktivity)
        {
            produkcni_listy produkcni_listy = new produkcni_listy();
            produkcni_listy.pk_id = pk_id;
            produkcni_listy.jmeno_aktivity = jmeno_aktivity;
            return produkcni_listy;
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
        public global::System.String jmeno_aktivity
        {
            get
            {
                return _jmeno_aktivity;
            }
            set
            {
                Onjmeno_aktivityChanging(value);
                ReportPropertyChanging("jmeno_aktivity");
                _jmeno_aktivity = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("jmeno_aktivity");
                Onjmeno_aktivityChanged();
            }
        }
        private global::System.String _jmeno_aktivity;
        partial void Onjmeno_aktivityChanging(global::System.String value);
        partial void Onjmeno_aktivityChanged();

        #endregion

    
    }

    #endregion

    
}