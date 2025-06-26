import { Component } from '@angular/core';
import { WarehouseStateService } from '../../services/warehouse-state-service';
import { WarehouseStateChanged } from '../../models/warehouse-state-changed';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { MatCard, MatCardTitle, MatCardContent, MatCardSubtitle, MatCardHeader } from '@angular/material/card';
import { ComponentType } from '../../models/component-type';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-warehouse-event',
  imports: [MatCard, MatCardTitle, MatCardContent, DatePipe, MatCardSubtitle, MatCardHeader, NgIf, NgFor, MatPaginator],
  templateUrl: './warehouse-event.html',
  styleUrl: './warehouse-event.css'
})
export class WarehouseEvent {
  /**
   *
   */
  constructor(private readonly service: WarehouseStateService) { }

  incoming: WarehouseStateChanged[] = [];
  outgoing: WarehouseStateChanged[] = [];

  totalNumberIn: number = 10;
  pageSizeIn: number = 3;
  pageNumberIn: number = 1;
  totalNumberOut: number = 10;
  pageSizeOut: number = 3;
  pageNumberOut: number = 1;

  async ngOnInit() {
   this.refresh();
  }


   private refresh() {

        this.service.getIncomingElements(this.pageNumberIn, this.pageSizeIn)
          .subscribe(data => {
            this.incoming = data.payload;
            this.totalNumberIn = data.totalNumber;
          });

        this.service.getOutgoingElements(this.pageNumberOut, this.pageSizeOut)
          .subscribe(data => {
              this.outgoing = data.payload;
              this.totalNumberOut = data.totalNumber;
          });
  }

  onPageChangeIn(event: PageEvent) {
  this.pageNumberIn = event.pageIndex + 1; 
  this.pageSizeIn = event.pageSize;

  this.refresh();           
}

  onPageChangeOut(event: PageEvent) {
  this.pageNumberOut = event.pageIndex + 1; 
  this.pageSizeOut = event.pageSize;

  this.refresh();           
}
}

