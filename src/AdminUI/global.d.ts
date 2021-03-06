declare module "*.module.less" {
    const classes: { [key: string]: string };
    export default classes;
}

declare module 'downloadjs' {
    export default function download(
      data: string | File | Blob | Uint8Array,
      filename?: string,
      mimeType?: string
    ): XMLHttpRequest | boolean;
  }
  