class Ticket {
  constructor({
    subject,
    condition,
    background,
    content,
    reason,
    declined_reason,
    status_id,
    user_id,
    priority_id,
    date_created,
    date_approved,
    date_declined,
    signatories,
    files,
  }) {
    (this.subject = subject),
      (this.condition = condition),
      (this.background = background),
      (this.content = content),
      (this.reason = reason),
      (this.declined_reason = declined_reason),
      (this.status_id = status_id),
      (this.user_id = user_id),
      (this.priority_id = priority_id),
      (this.date_created = date_created),
      (this.date_approved = date_approved),
      (this.date_declined = date_declined),
      (this.signatories = signatories),
      (this.files = files);
  }
}

export { Ticket };
