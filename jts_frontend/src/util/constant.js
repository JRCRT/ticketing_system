const FILE_STATUS = Object.freeze({
  INITIAL: "INITIAL",
  LOADING: "LOADING",
});

const EXTENSIONS = Object.freeze({
  DOCX: "docx",
  DOC: "doc",
  PDF: "pdf",
  XLS: "xls",
  XLSX: "xlsx",
});

const TICKET_STATUS = Object.freeze({
  PENDING: "Pending",
  APPROVED: "Approved",
  DECLINED: "Declined",
});

const SIGNATORY_TYPE = Object.freeze({
  APPROVER: "Approver",
  PARTY: "Party",
  CHECKER: "Checker",
});

export { FILE_STATUS, EXTENSIONS, TICKET_STATUS, SIGNATORY_TYPE };
