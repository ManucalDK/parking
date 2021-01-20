import { RegisterService } from '../../../services/entry/register.service';
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroupDirective, Validators } from '@angular/forms';
import { registerFormModel } from '../../../models/entry/registerFormModel';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { DialogMessageComponent } from '../../../shared/components/dialog-message/dialog-message.component';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {
  
  registerForm = this.fb.group({
    placa: [null, Validators.required],
    tipoVehiculo: [null, Validators.required],
    cilindraje: [null]
  });

  hasUnitNumber = false;
  isMotorcycle = true;
  generalError:string = "";

  states = [
    {name: 'Carro', abbreviation: 1},
    {name: 'Moto', abbreviation: 2},
  ];

  @ViewChild(FormGroupDirective)
  formGroupDirective!: FormGroupDirective;

  constructor(private fb: FormBuilder,
              private registerService: RegisterService,
              public dialog: MatDialog) {}

  async onSubmit() {
    this.generalError = "";
    const product = await this.registerEntryAsync();
    this.openDialog();
    if(!this.generalError){
      this.formGroupDirective.resetForm();  
    }
  }

  async registerEntryAsync(){
    const registerForm:registerFormModel = {
      IdVehicle:this.registerForm.get('placa')?.value,
      CC:this.registerForm.get('cilindraje')?.value,
      IdVehicleType: this.registerForm.get('tipoVehiculo')?.value,
    };
    
    return this.registerService.createEntry(registerForm).toPromise().catch((reason: HttpErrorResponse) => {
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
