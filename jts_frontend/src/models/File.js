import { FILE_STATUS } from "@/util/constant";
class File {
  constructor(file) {
    this.file = file;
    this.id = `${file.name}-${file.size}-${file.lastModified}-${file.type}`;
    this.url = URL.createObjectURL(file);
    this.status = FILE_STATUS.INITIAL;
  }
}

export { File };
