import { NotfoundComponent } from './shared/components/errors/notfound/notfound.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PlayComponent } from './play/play.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'play', component: PlayComponent },
  { path: 'not-found', component: NotfoundComponent },
  {
    path: 'account',
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
  },
  { path: '**', component: NotfoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
