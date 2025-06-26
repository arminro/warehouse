import { Component, Inject } from "@angular/core";
import { FormGroup, FormBuilder, Validators, FormArray, ReactiveFormsModule } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from "@angular/material/dialog";
import { ComponentType } from "../../models/component-type";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";

@Component({
  selector: 'app-component-type-dialog',
  templateUrl: './component-type-dialog.html',
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule]
})
export class CreateComponentTypeDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CreateComponentTypeDialog>,
    @Inject(MAT_DIALOG_DATA) public data?: ComponentType
  ) {
        this.form = this.fb.group({
      name: [this.data?.name || '', Validators.required],
      priceInHungarianForints: [this.data?.priceInHungarianForints || 0, Validators.required],
      description: [this.data?.description || ''],
      massInGrams: [this.data?.massInGrams || 0, Validators.required]
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
