import { Component } from '@angular/core';
import { WarehouseStatisticsService } from '../../services/warehouse-statistics-service';
import { WarehouseStatisticsModel } from '../../models/warehouse-statistics.model';
import { MatCard, MatCardContent, MatCardHeader, MatCardSubtitle, MatCardTitle } from '@angular/material/card';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-warehouse-statistics',
  imports: [MatCard, MatCardTitle, MatCardContent, DecimalPipe, MatCardSubtitle, MatCardHeader],
  templateUrl: './warehouse-statistics.html',
  styleUrl: './warehouse-statistics.css'
})
export class WarehouseStatistics {
    stats?: WarehouseStatisticsModel

    constructor(private readonly warehouseStatisticsService: WarehouseStatisticsService) {}

      async ngOnInit() {
        this.warehouseStatisticsService.getStatistics()
        .subscribe(data => this.stats = data);
  }

}

