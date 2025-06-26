import { ComponentType } from '../../models/component-type';
import { ComponentTypeService } from '../../services/component-type-service';
import { CommonModule, DecimalPipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatChip, MatChipListbox, MatChipRemove } from '@angular/material/chips';
import { MatExpansionModule, MatExpansionPanelHeader, MatExpansionPanelTitle } from '@angular/material/expansion';
import { PurchaseDialogComponent } from '../purchase-dialog/purchase-dialog';
import { MatDialog } from '@angular/material/dialog';
import { Component, NgZone } from '@angular/core';
import { ComponentModel } from '../../models/component-model';
import { ComponentService } from '../../services/component-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CreateComponentTypeDialog as ComponentTypeDialog } from '../component-type-dialog/component-type-dialog';
import { CreateComponentDialog } from '../create-component-dialog/create-component-dialog';
import { v4 as uuidv4 } from 'uuid';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-warehouse-home',
  imports: [DecimalPipe, CommonModule, MatCardModule, MatButtonModule, MatToolbarModule, MatIconModule, MatListModule, MatChip, MatChipRemove,  MatChipListbox, MatExpansionModule, MatExpansionPanelTitle, MatExpansionPanelHeader, MatPaginator],
  templateUrl: './warehouse-home.html',
  styleUrl: './warehouse-home.css'
})
export class WarehouseHome {


    constructor(private readonly compTypeService: ComponentTypeService, private readonly compService: ComponentService, private readonly dialog: MatDialog, private snackBar: MatSnackBar) { 
    }
    catalog?: ComponentType[];
    pageNumber: number = 1;
    pageSize: number = 2;
    totalNumber: number = 0;

    async ngOnInit() {
       this.refresh();
  }

  addComponentType() {
    const dialogRef = this.dialog.open(ComponentTypeDialog, {
    width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.compTypeService.createElement(result)
              .subscribe({
                next: () => {
                  this.snackBar.open(`${result.name} created`, 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-success']
                  });
                      this.refresh();
              },
                error: err => {
                  this.snackBar.open(`Creating ${result.name} failed: ${err.message}`, 'Dismiss', {
                    duration: 3000,
                    panelClass: ['snackbar-error']
                  });
            }
        });      
      }
    });
  
}

editComponentType(type: ComponentType) {
    const dialogRef = this.dialog.open(ComponentTypeDialog, {
    width: '600px',
    data: type
    });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          result.id = type.id; // the template does not know about the id
          this.compTypeService.updateElement(result)
                .subscribe({
                  next: () => {
                    this.snackBar.open(`${result.name} updated`, 'Close', {
                      duration: 3000,
                      panelClass: ['snackbar-success']
                    });

                 this.refresh();
                  
                },
                  error: err => {
                    this.snackBar.open(`Updating ${result.name} failed: ${err.message}`, 'Dismiss', {
                      duration: 3000,
                      panelClass: ['snackbar-error']
                    });
              }
          });      
        }
  });
}

deleteComponentType(type: ComponentType) {
    const dialogRef = this.dialog.open(PurchaseDialogComponent, {
    width: '350px',
    data: { message: `Removing ${type.name} from the catalog?` }
  });

    dialogRef.afterClosed().subscribe(result => {
    if (result === true) {
        this.compTypeService.deleteElement(type.id)
          .subscribe({
            next: () => {
              this.snackBar.open(`${type.name} removed`, 'Close', {
                duration: 3000,
                panelClass: ['snackbar-success']
              });

              this.refresh();
              
            },
            error: err => {
              this.snackBar.open(`Remoing ${type.name} failed: ${err.message}`, 'Dismiss', {
                duration: 3000,
                panelClass: ['snackbar-error']
              });
          }
      });
  }});
}



addComponent(type: ComponentType) {

  var suggestedComponent = new ComponentModel( { catalogId:  uuidv4()}); // we suggest a new UUID to the user

   const dialogRef = this.dialog.open(CreateComponentDialog, {
    width: '600px',
    data: suggestedComponent
    });

        dialogRef.afterClosed().subscribe(result => {
        if (result) {
            result.componentTypeId = type.id;
            console.log(result);
            this.compService.createElement(result)
              .subscribe({
                next: () => {
                  this.snackBar.open(`Component with catalog number ${result.catalogId} added`, 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-success']
                  });

                this.refresh();
                  
                },
                error: err => {
                  this.snackBar.open(`Add the component with catalog number ${result.catalogId} failed: ${err.message}`, 'Dismiss', {
                    duration: 3000,
                    panelClass: ['snackbar-error']
                  });
              }
          });
  }});

}

deleteComponent(type: ComponentType, component: ComponentModel) {
   const dialogRef = this.dialog.open(PurchaseDialogComponent, {
    width: '350px',
    data: { message: `Buying ${component.catalogId} (${type.name})?` }
  });

    dialogRef.afterClosed().subscribe(result => {
    if (result === true) {
        this.compService.deleteElement(component.componentId)
          .subscribe({
            next: () => {
              this.snackBar.open(`${component.catalogId} purchased`, 'Close', {
                duration: 3000,
                panelClass: ['snackbar-success']
              });

              this.refresh();
              
            },
            error: err => {
              this.snackBar.open(`Purchasing ${component.catalogId} failed: ${err.message}`, 'Dismiss', {
                duration: 3000,
                panelClass: ['snackbar-error']
              });
          }
      });
  }});
}

  private refresh() {
  this.compTypeService.getElements(this.pageNumber, this.pageSize)
        .subscribe(data => {
          this.catalog = data.payload
          this.totalNumber = data.totalNumber;
        });
  }

onPageChange(event: PageEvent) {
  this.pageNumber = event.pageIndex+1; // of course, it starts on 0, so we have to make it humanly readable
  this.pageSize = event.pageSize;

  this.refresh();
              
}
}

