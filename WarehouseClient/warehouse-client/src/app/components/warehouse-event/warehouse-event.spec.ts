import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseEvent } from './warehouse-event';

describe('WarehouseEvent', () => {
  let component: WarehouseEvent;
  let fixture: ComponentFixture<WarehouseEvent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseEvent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarehouseEvent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
