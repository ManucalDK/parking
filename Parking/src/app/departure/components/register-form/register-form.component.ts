import { DepartureService } from '../../../services/departure/departure.service';
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroupDirective, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { registerFormModel } from '../../../models/departure/registerFormModel';
import { MatDialog } from '@angular/material/dialog';
import { DialogMessageComponent } from '../../../shared/components/dialog-message/dialog-message.component';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {
  registerForm = this.fb.group({
    placa: [null, Validators.required]
  });

  hasUnitNumber = false;
  generalError = "";
  submitButtonDisable = false;

  @ViewChild(FormGroupDirective)
  formGroupDirective!: FormGroupDirective;

  constructor(private fb: FormBuilder,
    private departureService: DepartureService,
    public dialog: MatDialog) {}

  async onSubmit() {
    this.generalError = "";
    this.submitButtonDisable = true;
    const product = await this.registerEntryAsync();
    this.submitButtonDisable = false;
    this.openDialog();
    if(!this.generalError){
      this.formGroupDirective.resetForm();  
    }
  }

  async registerEntryAsync(){
    const registerForm:registerFormModel = {
      IdVehicle:this.registerForm.get('placa')?.value,
    };
    
    return this.departureService.createEntry(registerForm).toPromise().catch((reason: HttpErrorResponse) => {
      this.generalError = reason.error;
    });
  }

  openDialog() {
    let error:boolean = this.generalError?true:false;
    this.dialog.open(DialogMessageComponent, {
      data: {
        title: error?"Ocurrio un error":'Registro guardado con exito',
        message: error?this.generalError:''
      }
    });
  }
}
