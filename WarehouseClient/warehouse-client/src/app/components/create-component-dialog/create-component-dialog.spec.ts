import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateComponentDialog } from './create-component-dialog';

describe('CreateComponentDialog', () => {
  let component: CreateComponentDialog;
  let fixture: ComponentFixture<CreateComponentDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateComponentDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateComponentDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
