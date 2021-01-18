import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { 
        path: 'home', 
        redirectTo: '', 
        pathMatch: 'full' 
      },
      { 
        path: 'entry', 
        loadChildren: () => import('./entry/entry.module').then(m => m.EntryModule)
      },
      { 
        path: 'departure', 
        loadChildren: () => import('./departure/departure.module').then(m => m.DepartureModule)
      }
    ]
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
