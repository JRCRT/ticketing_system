import { FILE_STATUS } from "@/util/constant";
import { setIcon } from "@/util/helper";
class File {
  constructor(file) {
    this.file = file;
    this.id = `${file.name}-${file.size}-${file.lastModified}-${file.type}`;
    this.url = setIcon(file);
    this.status = FILE_STATUS.INITIAL;
  }
}

export { File };
