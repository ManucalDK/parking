import { DepartureService } from '../../../services/departure/departure.service';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { registerFormModel } from '../../../models/departure/registerFormModel';

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

  constructor(private fb: FormBuilder,
    private departureService: DepartureService) {}

  async onSubmit() {
    this.generalError = "";
    const product = await this.registerEntryAsync();
  }

  async registerEntryAsync(){
    const registerForm:registerFormModel = {
      IdVehicle:this.registerForm.get('placa')?.value,
    };
    
    return this.departureService.createEntry(registerForm).toPromise().catch((reason: HttpErrorResponse) => {
      this.generalError = reason.error;
    });
  }
}
