import fs from 'fs'

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const saveSchemas = async () => {
  var schemas: string[] = []
  const json: OpenAPI = await getJson();
  for (const path in json.paths) {
    const pathItem = json.paths[path];
    for (const operation in pathItem) {
      const operationItem = pathItem[operation];
      const tagName = operationItem.tags[0].replace(/Module:/i, "").trim()
      if (schemas.filter(x => x == tagName).length == 0) schemas.push(tagName)
    }
  }
  if (schemas.length == 0) return;
  fs.writeFile("schemas.json", JSON.stringify(schemas), function (err) {
    if (err) {
      console.log(err);
    }
  })
}

const getJson = async () => {
  return fetch('https://localhost:7098/openapi/v1.json')
    .then((response) => response.json()).then(r => r).catch(error => console.log(error))
}
interface Operation {
  tags: string[];
  summary: string;
  operationId: string;
  responses: {
    [statusCode: string]: Response;
  };
}
interface PathItem {
  [operation: string]: Operation; // get, post, put, delete, etc.
}
interface Paths {
  [path: string]: PathItem;
}
interface OpenAPI {
  paths: Paths;
}

saveSchemas();


