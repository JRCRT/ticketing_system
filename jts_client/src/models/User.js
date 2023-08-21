class User {
  constructor({
    first_name,
    middle_name,
    last_name,
    username,
    password,
    email,
    role_id,
    department_id,
    job_title_id,
  }) {
    this.first_name = first_name ?? "";
    this.middle_name = middle_name ?? "";
    this.last_name = last_name ?? "";
    this.username = username ?? "";
    this.password = password ?? "";
    this.email = email ?? "";
    this.role_id = role_id ?? 1;
    this.department_id = department_id ?? 1;
    this.job_title_id = job_title_id ?? 1;
  }
}

export { User };
