import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { AppConfig } from './app/configuration/app-config';
import { provideHttpClient } from '@angular/common/http';


export async function loadConfigFactory(configService: AppConfig): Promise<void> {
  await configService.loadConfigAsync();
}

bootstrapApplication(App, appConfig)
  .catch((err) => console.error(err));

