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

const TICKET_STATUS_ID = Object.freeze({
  PENDING: 1,
  APPROVED: 2,
  REJECTED: 3,
  DONE: 4,
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

export {
  EXTENSIONS,
  TICKET_STATUS,
  TICKET_STATUS_ID,
  SIGNATORY_TYPE,
  ROLE,
  JOB_TITLE,
};
