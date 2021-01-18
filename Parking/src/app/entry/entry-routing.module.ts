import { RegisterFormComponent } from './components/register-form/register-form.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestNavComponent } from './components/test-nav/test-nav.component';
import { ListComponent } from './components/list/list.component';

const routes: Routes = [
  { path: '', component: TestNavComponent, children: [
    { path: 'register', component: RegisterFormComponent },
    { path: 'list', component: ListComponent },
  ] }
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EntryRoutingModule { }
