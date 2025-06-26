import { APP_INITIALIZER, ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { AppConfig } from './configuration/app-config';
import { loadConfigFactory } from '../main';
import { ComponentType } from './models/component-type';
import { ComponentTypeService } from './services/component-type-service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    AppConfig,
    // {
    //   provide: APP_INITIALIZER,
    //   useFactory: loadConfigFactory,
    //   deps: [AppConfig],
    //   multi: true,
    // }
  ]
};
