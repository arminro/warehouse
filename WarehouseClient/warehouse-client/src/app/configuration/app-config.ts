import { Injectable } from "@angular/core";
import { IAppConfig } from "./interfaces/iapp-config";
import { environment } from "../environments/environment";
import { HttpClient } from "@angular/common/http";
import { firstValueFrom } from "rxjs";

@Injectable()
export class AppConfig {
    private settings?: IAppConfig;

    constructor(private http: HttpClient) { }
     async loadConfigAsync(): Promise<void> {
        const env = environment.production ? 'prod' : 'dev';
        const jsonFile = `assets/configuration/config.${env}.json`;
        const cfg = await firstValueFrom(this.http.get(jsonFile));
        this.settings = cfg as IAppConfig;
    }

    getSettings(): IAppConfig {
        if (!this.settings) {
            throw new Error("Configuration not loaded. Call loadConfigAsync() first.");
        }
        return this.settings;
    }
}


   