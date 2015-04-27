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
    public partial class dbContact : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new dbContact object using the connection string found in the 'dbContact' section of the application configuration file.
        /// </summary>
        public dbContact() : base("name=dbContact", "dbContact")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbContact object.
        /// </summary>
        public dbContact(string connectionString) : base(connectionString, "dbContact")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbContact object.
        /// </summary>
        public dbContact(EntityConnection connection) : base(connection, "dbContact")
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
        public ObjectSet<kontakty> kontakty
        {
            get
            {
                if ((_kontakty == null))
                {
                    _kontakty = base.CreateObjectSet<kontakty>("kontakty");
                }
                return _kontakty;
            }
        }
        private ObjectSet<kontakty> _kontakty;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the kontakty EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTokontakty(kontakty kontakty)
        {
            base.AddObject("kontakty", kontakty);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="dbContactModel", Name="kontakty")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class kontakty : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new kontakty object.
        /// </summary>
        /// <param name="pk_id">Initial value of the pk_id property.</param>
        /// <param name="jmeno">Initial value of the jmeno property.</param>
        /// <param name="prijmeni">Initial value of the prijmeni property.</param>
        /// <param name="email">Initial value of the email property.</param>
        /// <param name="telefon">Initial value of the telefon property.</param>
        public static kontakty Createkontakty(global::System.Int32 pk_id, global::System.String jmeno, global::System.String prijmeni, global::System.String email, global::System.String telefon)
        {
            kontakty kontakty = new kontakty();
            kontakty.pk_id = pk_id;
            kontakty.jmeno = jmeno;
            kontakty.prijmeni = prijmeni;
            kontakty.email = email;
            kontakty.telefon = telefon;
            return kontakty;
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
        public global::System.String prijmeni
        {
            get
            {
                return _prijmeni;
            }
            set
            {
                OnprijmeniChanging(value);
                ReportPropertyChanging("prijmeni");
                _prijmeni = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("prijmeni");
                OnprijmeniChanged();
            }
        }
        private global::System.String _prijmeni;
        partial void OnprijmeniChanging(global::System.String value);
        partial void OnprijmeniChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String email
        {
            get
            {
                return _email;
            }
            set
            {
                OnemailChanging(value);
                ReportPropertyChanging("email");
                _email = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("email");
                OnemailChanged();
            }
        }
        private global::System.String _email;
        partial void OnemailChanging(global::System.String value);
        partial void OnemailChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String telefon
        {
            get
            {
                return _telefon;
            }
            set
            {
                OntelefonChanging(value);
                ReportPropertyChanging("telefon");
                _telefon = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("telefon");
                OntelefonChanged();
            }
        }
        private global::System.String _telefon;
        partial void OntelefonChanging(global::System.String value);
        partial void OntelefonChanged();

        #endregion

    
    }

    #endregion

    
}
