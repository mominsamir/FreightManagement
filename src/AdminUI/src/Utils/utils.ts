const createChunks = function (list: Array<any>, size: number) {
  if (list == null) {
    return [];
  }
  let index = 0;
  let chunks = [];
  while (index < list.length) {
    chunks.push(list.slice(index, index + size));
    index += size;
  }
  return chunks;
};

const toPrice = (p: number) => (isNaN(p) ? p : p.toFixed(2));

const toPriceSix = (p: number) => (isNaN(p) ? p : p.toFixed(6));

const enumToString = (s: string) => {
  if (!s) {
    return '';
  }
  let str = s.toLowerCase().split('');
  str[0] = str[0].toUpperCase();
  let idx = -1;
  do {
    if (idx > 0) str[idx] = ' ';
    str[idx + 1] = str[idx + 1].toUpperCase();
    idx = str.indexOf('_', idx + 1);
  } while (idx !== -1 && idx < s.length);
  return str.join('');
};

const isObjectEmpty = function (obj: object) {
  return Object.getOwnPropertyNames(obj).length >= 1;
};

const cleanStr = (val: string, defVal?: string) => {
  if (!defVal) defVal = '';
  if (!val) return defVal;
  let trimmedVal = val.trim();
  return trimmedVal !== '' ? trimmedVal : defVal;
};

export { createChunks, toPrice, toPriceSix, isObjectEmpty, cleanStr, enumToString };
