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
  }) {
    this.first_name = first_name ?? "";
    this.middle_name = middle_name ?? "";
    this.last_name = last_name ?? "";
    this.username = username ?? "";
    this.password = password ?? "";
    this.email = email ?? "";
    this.role_id = role_id ?? 0;
    this.department_id = department_id ?? 0;
  }
}

export { User };
