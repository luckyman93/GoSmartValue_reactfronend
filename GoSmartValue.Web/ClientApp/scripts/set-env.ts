const { writeFile } = require('fs');

//Must gitIgnore
const targetPath = './src/environments/environment.custom.ts';

// Load dotenv to work with process.env
require('dotenv').config();

// environment.ts file structure
const envConfigFile = `
export const environment = {
 production: false,
 apiUrl: '${process.env.API_URL}',
 apiKey: '${process.env.API_KEY}'
};
`;
writeFile(targetPath, envConfigFile, function (err:any) {
 if (err) {
  throw console.error(err);
 } else {
  console.log('Using custom environment variables');
}
});