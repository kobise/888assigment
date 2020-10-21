import { TestBed } from '@angular/core/testing';

import { ContinentResolver } from './continent.resolver';

describe('ContinentResolver', () => {
  let resolver: ContinentResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(ContinentResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
