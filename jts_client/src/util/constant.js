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
  REJECTED: "Rejected",
  DONE: "Done",
});

const ROLE = Object.freeze({
  ADMIN: "Admin",
  APPROVER: "Approver",
  CHECKER: "Checker",
  USER: "User",
});

const SIGNATORY_TYPE = Object.freeze({
  APPROVER: "Approver",
  PARTY: "Party",
  CHECKER: "Checker",
});

const JOB_TITLE = Object.freeze({
  SVP: "SVP",
  EVP: "EVP",
  CFO: "CFO",
  President: "President",
});

export { EXTENSIONS, TICKET_STATUS, SIGNATORY_TYPE, ROLE, JOB_TITLE };
