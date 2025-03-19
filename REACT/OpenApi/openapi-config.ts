import type { ConfigFile } from '@rtk-query/codegen-openapi'
import { EndpointMatcherFunction, OutputFileOptions } from '@rtk-query/codegen-openapi/lib/types';
import schemas from './schemas.json';

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const pathMatcher = (pattern: RegExp): EndpointMatcherFunction => {
  return (operationName, operationDefinition) => {
    return pattern.test(operationDefinition.operation.tags[0]);
  };
}

const getOutputFileOptions = () => {
  const outputFileOptions: { [outputFile: string]: Omit<OutputFileOptions, 'outputFile'> } = schemas.reduce((acc, schema) => {
    acc[`./src/store/api/${schema}.ts`] = { filterEndpoints: pathMatcher(new RegExp(`${schema}`, "i")) };
    return acc;
  }, {});

  return outputFileOptions;
};
const config: ConfigFile = {
  schemaFile: 'https://localhost:7098/openapi/v1.json',
  apiFile: './src/store/emptyApi.ts',
  apiImport: 'emptySplitApi',
  exportName: 'kubis1982Api',
  hooks: false,
  argSuffix: "",
  tag: true,
  outputFiles: getOutputFileOptions()
}

export default config