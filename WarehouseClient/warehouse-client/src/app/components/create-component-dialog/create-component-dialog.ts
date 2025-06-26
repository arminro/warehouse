import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { ComponentModel } from '../../models/component-model';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-component-model-dialog',
  templateUrl: './create-component-dialog.html',
  imports: [ MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, NgIf]
})
export class CreateComponentDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CreateComponentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ComponentModel
  ) {
    this.form = this.fb.group({
      catalogId: [data?.catalogId ?? '', Validators.required]
    });
  }

   save() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  cancel() {
    this.dialogRef.close();
  }
}
