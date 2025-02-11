using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MedicalResearch.BillingData.Model {

public partial class BillableItem {

  /// <summary> a global unique id of a concrete study-visit execution which is usually originated at the primary CRF or study management system ('SMS') </summary>
  [Required]
  public Guid BillableItemUid { get; set; } = Guid.NewGuid();

  /// <summary> a global unique id of a concrete study execution (dedicated to a concrete institute) which is usually originated at the primary CRF or study management system ('SMS') </summary>
  [Required]
  public Guid StudyExecutionIdentifier { get; set; }

  /// <summary> identity of the patient which can be a randomization or screening number (the exact semantic is defined per study) *this field has a max length of 50 </summary>
  [MaxLength(50), Required]
  public String ParticipantIdentifier { get; set; }

  /// <summary> unique invariant name of the visit-procedure as defined in the 'StudyWorkflowDefinition' (originated from the sponsor) </summary>
  [Required]
  public String VisitProcedureName { get; set; }

  /// <summary> title of the visit execution as defined in the 'StudyWorkflowDefinition' (originated from the sponsor) </summary>
  [Required, IdentityLabel]
  public String UniqueExecutionName { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> ExecutionEndDateUtc { get; set; }

  /// <summary> *this field is optional (use null as value) </summary>
  public String Description { get; set; }

  /// <summary> One of the following values: 'General' / 'Site' / 'Paticipant' (Requires a ParticipantIdentifier) / 'Visit' (Requires a ParticipantIdentifier and UniqueExecutionName) </summary>
  [Required]
  public String RelatedTo { get; set; }

}

public partial class StudyExecutionScope {

  /// <summary> a global unique id of a concrete study execution (dedicated to a concrete institute) which is usually originated at the primary CRF or study management system ('SMS') </summary>
  [FixedAfterCreation, Required, IdentityLabel]
  public Guid StudyExecutionIdentifier { get; set; } = Guid.NewGuid();

  /// <summary> the institute which is executing the study (this should be an invariant technical representation of the company name or a guid) </summary>
  [FixedAfterCreation, Required]
  public String ExecutingInstituteIdentifier { get; set; }

  /// <summary> the official invariant name of the study as given by the sponsor *this field has a max length of 100 </summary>
  [FixedAfterCreation, MaxLength(100), Required]
  public String StudyWorkflowName { get; set; }

  /// <summary> version of the workflow *this field has a max length of 20 </summary>
  [FixedAfterCreation, MaxLength(20), Required]
  public String StudyWorkflowVersion { get; set; }

  /// <summary> optional structure (in JSON-format) containing additional metadata regarding this record, which can be used by 'StudyExecutionSystems' to extend the schema *this field is optional (use null as value) </summary>
  public String ExtendedMetaData { get; set; }

  [Required]
  public Decimal SiteRelatedTaxPercentage { get; set; }

  /// <summary> ISO 3-Letter Code (USD, EUR, ...) </summary>
  [Required]
  public String SiteRelatedCurrency { get; set; }

}

/// <summary> created by the sponsor </summary>
public partial class BillingDemand {

  [Required]
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  public String OfficialNumber { get; set; }

  [Required]
  public Guid StudyExecutionIdentifier { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> TransmissionDateUtc { get; set; }

  [Required]
  public DateTime CreationDateUtc { get; set; }

  [Required]
  public String CreatedByPerson { get; set; }

}

/// <summary> Respresents a Snapshot, containig al the values, which are required to be fixed in relation to a concrete invoice or demand </summary>
public partial class BillingItem {

  [Required]
  public Int64 BillingItemId { get; set; }

  [Required]
  public DateTime CreationDateUtc { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> SponsorValidationDateUtc { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> ExecutorValidationDateUtc { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<Guid> BillingDemandId { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<Guid> InvoiceId { get; set; }

  [Required]
  public Int32 FixedExecutionState { get; set; }

  /// <summary> Including 'FixedPriceOfTasks' but excluding Taxes </summary>
  [Required]
  public Decimal FixedPriceOfItem { get; set; }

  /// <summary> An additional info which is only relevant when declaing Subtasks </summary>
  [Required]
  public Decimal FixedPriceOfTasks { get; set; }

  [Required]
  public Decimal FixedTaxPercentage { get; set; }

  [Required]
  public String TasksRelatedInfo { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<Guid> BillableItemUid { get; set; }

  [Required]
  public String Description { get; set; }

}

/// <summary> created by the executor-company </summary>
public partial class Invoice {

  [FixedAfterCreation, Required]
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary> the invoice number </summary>
  [FixedAfterCreation, Required, IdentityLabel]
  public String OfficialNumber { get; set; }

  [FixedAfterCreation, Required]
  public Guid StudyExecutionIdentifier { get; set; }

  [FixedAfterCreation, Required]
  public DateTime OffcialInvoiceDate { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> TransmissionDateUtc { get; set; }

  [Required]
  public DateTime CreationDateUtc { get; set; }

  [Required]
  public String CreatedByPerson { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> PaymentSubmittedDateUtc { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<DateTime> PaymentReceivedDateUtc { get; set; }

  /// <summary> *this field is optional </summary>
  public Nullable<Guid> CorrectionOfInvoiceId { get; set; }

}

}
