import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateComponentTypeDialog } from './component-type-dialog';

describe('CreateComponentTypeDialog', () => {
  let component: CreateComponentTypeDialog;
  let fixture: ComponentFixture<CreateComponentTypeDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateComponentTypeDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateComponentTypeDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
