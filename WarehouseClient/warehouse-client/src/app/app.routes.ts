import { Routes } from '@angular/router';
import { WarehouseHome } from './components/warehouse-home/warehouse-home';
import { WarehouseStatistics } from './components/warehouse-statistics/warehouse-statistics';
import { WarehouseEvent } from './components/warehouse-event/warehouse-event';

export const routes: Routes = [

    {
     path: 'storefront',
     component: WarehouseHome
    },

    {
     path: 'stat',
     component: WarehouseStatistics
    },
     {
     path: 'events',
     component: WarehouseEvent
    },

    {
        path: '',
        redirectTo: 'storefront',
        pathMatch: 'full'
    },
    {
        path: '**',
        redirectTo: 'storefront',
        pathMatch: 'full'
    }
];
