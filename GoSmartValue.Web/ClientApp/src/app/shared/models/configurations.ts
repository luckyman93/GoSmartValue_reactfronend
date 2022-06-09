export class Configuration {
  API_URL = '';
  HOST = '';
  API_KEY = '';
  FIRE_API_KEY = '';
  FIRE_AUTH_DOMAIN = '';
  FIRE_PROJECT_ID = '';
  FIRE_STORAGE_BUCKETID = '';
  FIRE_MESSAGING_SENDER_ID = '';
  FIRE_APP_ID = '';
  FIRE_MEASUREMENT_ID = '';

  setConfigValues(config: Configuration) {
    this.HOST = config.HOST;
    this.API_URL = config.API_URL;
    this.API_KEY = config.API_KEY;
    this.FIRE_API_KEY = config.FIRE_API_KEY;
    this.FIRE_AUTH_DOMAIN = config.FIRE_AUTH_DOMAIN;
    this.FIRE_PROJECT_ID = config.FIRE_PROJECT_ID;
    this.FIRE_STORAGE_BUCKETID = config.FIRE_STORAGE_BUCKETID;
    this.FIRE_MESSAGING_SENDER_ID = config.FIRE_MESSAGING_SENDER_ID;
    this.FIRE_APP_ID = config.FIRE_APP_ID;
    this.FIRE_MEASUREMENT_ID = config.FIRE_MEASUREMENT_ID;
  }

  get fireBaseSettings() {
    return {
      apiKey: this.FIRE_API_KEY,
      authDomain: this.FIRE_AUTH_DOMAIN,
      projectId: this.FIRE_PROJECT_ID,
      storageBucket: this.FIRE_STORAGE_BUCKETID,
      messagingSenderId: this.FIRE_MESSAGING_SENDER_ID,
      appId: this.FIRE_APP_ID,
      measurementId: this.FIRE_MEASUREMENT_ID,
    };
  }
}
