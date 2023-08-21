import { setIconUrl } from "@/util/helper";
class UploadedFile {
  constructor(file) {
    this.file = file;
    this.id = `${file.name}-${file.size}-${file.lastModified}-${file.type}`;
    this.url = setIconUrl(file) ?? URL.createObjectURL(file);
  }
}

export { UploadedFile };
