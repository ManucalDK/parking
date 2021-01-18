import { ListComponent } from './components/list/list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavComponent } from "./components/nav/nav.component";
import { RegisterFormComponent } from "./components/register-form/register-form.component";

const routes: Routes = [
  { path: '', component: NavComponent, children: [
    { path: 'register', component: RegisterFormComponent },
    { path: 'list', component: ListComponent },
  ] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartureRoutingModule { }
