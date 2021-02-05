import { RegisterService } from '../../../services/entry/register.service';
import { Component, OnInit, ViewChild } from '@angular/core';
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
export class RegisterFormComponent implements OnInit {
  
  registerForm = this.fb.group({
    placa: [null, Validators.required],
    tipoVehiculo: [null, Validators.required],
    cilindraje: [null]
  });

  hasUnitNumber = false;
  isMotorcycle = true;
  generalError:string = "";
  submitButtonDisable = false;

  states = [
    {name: 'Carro', abbreviation: 1},
    {name: 'Moto', abbreviation: 2},
  ];

  @ViewChild(FormGroupDirective)
  formGroupDirective!: FormGroupDirective;

  constructor(private fb: FormBuilder,
              private registerService: RegisterService,
              public dialog: MatDialog) {}
  ngOnInit(): void {
    this.registerService.getWeatherTest().subscribe(result => {
      console.log(result)
    })
  }

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
      CC:this.registerForm.get('cilindraje')?.value,
      IdVehicleType: this.registerForm.get('tipoVehiculo')?.value,
    };
    
    return this.registerService.createEntry(registerForm).toPromise().catch((reason: HttpErrorResponse) => {
      console.log(reason)
      this.generalError = reason?.error;
      if(reason?.error?.errors) {
        let errores = this.readErrors(reason.error.errors);
        errores.forEach((error:string, index) =>{
          this.generalError= error[index];
        })
      }
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

  readErrors(errors:[]){
    
    let vArray:any[] = [];
    const vErrors = errors;

    if(vErrors){
      for (const key in vErrors) {
        if (vErrors.hasOwnProperty(key)) {
          vArray.push(vErrors[key])
        }
      }
    }

    return vArray
  }

  getFieldValue(field:string){
    return this.registerForm.get(field)?.value;
  }
}
