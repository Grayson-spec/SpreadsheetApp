import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectMenuComponent } from './project-menu/project-menu.component';
import { OpenProjectComponent } from './open-project/open-project.component';
import { CreateNewProjectComponent } from './create-new-project/create-new-project.component';

export const routes: Routes = [
  { path: '', component: ProjectMenuComponent },
  { path: 'open-project', component: OpenProjectComponent },
  { path: 'create-new-project', component: CreateNewProjectComponent },
  { path: '**', redirectTo: '' }
];
export const appRoutes = RouterModule.forRoot(routes);
